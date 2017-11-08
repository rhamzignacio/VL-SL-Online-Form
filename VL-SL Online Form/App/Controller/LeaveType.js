angular.module("leaveType", [])

.controller("leaveTypeController", function ($scope, $location, $http, growl) {
    var vm = this;

    vm.LeaveTypeToBeDeleted = {};;

    SuccessMessage = function (message) {
        growl.success(message, { ttl: 2000 });
    }

    ErrorMessage = function (message) {
        growl.error(message, { title: "Error!", ttl: 3000 });
    }

    $scope.LeaveTypeDropDown = [
        { value: "VL", label: "Vacation Leave" },
        { value: "SL", label: "Sick Leave" }
    ];

    $scope.Init = function () {
        $http({
            method: "POST",
            url: "/LeaveType/GetLeaveType",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.LeaveTypes = data.data.leaveType;
        });
    }

    $scope.Clear = function () {
        vm.Modal = {};

        vm.Modal.Type = {};

        vm.Modal.Description = "";

        vm.Modal.LeaveDeduction = "";
    }

    $scope.SaveLeaveType = function (value) {
        var ifHasError = "N";

        if (value.Description === "" || value.Description === null) {
            ErrorMessage("Description is required");

            ifHasError = "Y";
        }

        if (value.Type === "" || value.Type === null) {
            ErrorMessage("Leave Type is required");

            ifHasError = "Y";
        }

        if (value.DaysBeforeFilling === "" || value.DaysBeforeFilling === null) {
            ErrorMessage("Days Before Filling is required");

            ifHasError = "Y";
        }

        if (ifHasError === "N") {
            $http({
                method: "POST",
                url: "/LeaveType/SaveLeaveType",
                data: { leaveType: value }
            }).then(function(data){
                if (data.data == "") {
                    SuccessMessage("Succesfully Saved");

                    $scope.Init();

                    $("#leaveTypeModal").modal('hide');
                }
                else {
                    ErrorMessage(data.data);
                }
            });
        }
    }

    $scope.DeleteLeaveType = function () {
        $http({
            method: "POST",
            url: "/LeaveType/DeleteLeaveType",
            data: { leaveType: vm.LeaveTypeToBeDeleted }
        }).then(function (data) {
            if (data.data == "") {
                SuccessMessage("Succesfully Deleted");

                $scope.Init();

                $("#deleteModal").modal('hide');
            }
            else {
                ErrorMessage(data.data);
            }
        })
    }

    $scope.SelectToDelete = function (value) {
        vm.LeaveTypeToBeDeleted = value;
    }
});