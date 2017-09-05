angular.module("leave", [])

.controller("leaveFormController", function($scope, $location, $http, growl){
    var vm = this;

    SuccessMessage = function (message) {
        growl.success(message, { ttl: 2000 });
    }

    ErrorMessage = function (message) {
        growl.error(message, { title: "Error!", ttl: 3000 });
    }

    $scope.LeaveTypeDropdown = [
        { value: "VL", label: "Vacation Leave" },
        { value: "VL-H", label: "Vacation Leave Half Day" },
        { value: "SL", label: "Sick Leave" },
        { value: "SL-H", label: "Sick Leave Half Day" },
        { value: "EL", label: "Emergency Leave" },
        { value: "EL-H", label: "Emergency Leave Half Day" },
        { value: "ML", label: "Maternity Leave" },
        { value: "PL", label: "Paternity Leave" },
        { value: "WL", label: "Leave without Pay" }
    ];

    $scope.Clear = function (value) {
        value = {};
    }

    var currentDate = new Date();

    GetUserDropDown = function () {
        $http({
            method: "POST",
            url: "/Leave/GetUserDropDown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.errorMessage);
            }
            else {
                vm.UserDropDown = data.data.users;
            }
        });
    }

    GetLeaveTypeDropDown = function () {
        $http({
            method: "POST",
            url: "/Leave/GetLeaveTypeDropdown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.errorMessage);
            }
            else {
                vm.TypeOfLeaveDropDown = data.data.leaveType;
            }
        });
    }

    $scope.InitFileForms = function () {

        GetUserDropDown();

        GetLeaveTypeDropDown();
    }

    $scope.LeaveEvent = function () {
        $http({
            method: "POST",
            url: "/Leave/GetLeaveTypeDetail",
            data: { ID: vm.Leave.Type }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                vm.MinDate = currentDate.getFullYear() + "-" + ("0" + (currentDate.getMonth() + 1)).slice(-2) + "-" + ("0" + (currentDate.getDate() + data.data.days)).slice(-2);
            }
        });
    }

    $scope.InitFiledForms = function () {
        $http({
            method: "POST",
            url: "/Leave/GetFiledForms",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                vm.FiledLeave = data.data.leave;

                vm.FiledOvertime = data.data.overtime;
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }

    $scope.SaveLeave = function (value) {
        var noError = "Y";

        if (value.Type == "" || value.Type == null) {
            noError = "N";

            ErrorMessage("Leave type is required");
        }

        if (value.StartDate == "" || value.StartDate == null) {
            noError = "N";

            ErrorMessage("Start date is required");
        }

        if (value.EndDate == "" || value.EndDate == null) {
            noError = "N";

            ErrorMessage("End date is required");
        }

        if (value.Reason == "" || value.Reason == null) {
            noError = "N";

            ErrorMessage("Reason is required");
        }

        if (noError == "Y") {
            $http({
                method: "POST",
                url: "/Leave/SaveLeave",
                data: { leave: value }
            }).then(function (data) {
                if (data.data.errorMessage == "") {
                    SuccessMessage("Successfully Saved");

                    vm.Leave = {};

                    window.location.href = "/Leave/FiledForms";
                }
                else {
                    ErrorMessage(data.data.errorMessage);
                }
            });
        }
    }

    $scope.LeaveToDelete = function (value) {
        vm.DeleteLeave = value;
    }

    $scope.OvertimeToDelete = function (value) {
        vm.DeleteOvertime = value;
    }

    $scope.SaveOvertime = function (value) {
        var noError = "Y";

        if (value.EffectiveDate == "" || value.EffectiveDate == null) {
            noError = "N";

            ErrorMessage("Date is required");
        }

        if (value.StartTime == "" || value.StartTime == null) {
            noError = "N";

            ErrorMessage("Start time is required");
        }

        if (value.EndTime == "" || value.EndTime == null) {
            noError = "N";

            ErrorMessage("End time is required");
        }

        if (value.Reason == "" || value.Reason == null) {
            noError = "N";

            ErrorMessage("Particulars is required");
        }

        if (noError == "Y") {
            $http({
                method: "POST",
                url: "/Leave/SaveOvertime",
                data: { overtime: value }
            }).then(function (data) {
                if (data.data.errorMessage == "") {
                    SuccessMessage("Successfully Saved");

                    vm.Overtime = {};
                }
                else {
                    ErrorMessage(data.data.errorMessage);
                }
            })
        }
    }

    $scope.CancelOvertime = function () {
        $http({
            method: "POST",
            url: "/Leave/CancelOvertime",
            data: { overtime: vm.DeleteOvertime }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                SuccessMessage("Successfully cancelled overtime");

                vm.DeleteOvertime.Status = "X";
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }

    $scope.CancelLeave = function () {
        $http({
            method: "POST",
            url: "/Leave/CancelLeave",
            data: { leave: vm.DeleteLeave }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                SuccessMessage("Successfully cancelled leave");

                vm.DeleteLeave.Status = "X";
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }

    //=====================FOR APPROVAL=====================
    $scope.InitForApproval = function () {
        $http({
            method: "POST",
            url: "/Leave/GetForApproval",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                vm.FiledLeave = data.data.leave;

                vm.FiledOvertime = data.data.ot;
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }

    $scope.DeclineOvertime = function () {
        if (vm.ModalOvertime.DeclineReason === null || vm.ModalOvertime.DeclineReason === "") {
            ErrorMessage("Reason is required");
        }
        else {


            $http({
                method: "POST",
                url: "/Leave/DeclineOvertime",
                data: { overtime: vm.ModalOvertime }
            }).then(function (data) {
                if (data.data.errorMessage == "") {
                    SuccessMessage("Successfully declined overtime")

                    vm.ModalOvertime.Status = "D";

                    $("#declineLeave").modal('hide');
                }
                else {
                    ErrorMessage(data.data.errorMessage);
                }
            });
        }
    }

    $scope.ApproveOvertime = function () {
        $http({
            method: "POST",
            url: "/Leave/ApproveOvertime",
            data: { overtime: vm.ModalOvertime }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                SuccessMessage("Successfully approved overtime")

                vm.ModalOvertime.Status = "A";

                $("#approveOvertime").modal('hide');
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        })
    }

    $scope.AssignOvertime = function (value) {
        vm.ModalOvertime = value;
    }

    $scope.AssignLeave = function (value) {
        vm.ModalLeave = value;
    }

    $scope.ApproveLeave = function () {
        $http({
            method: "POST",
            url: "/Leave/ApproveLeave",
            data: { leave: vm.ModalLeave }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                SuccessMessage("Successfully approved leave")

                vm.ModalLeave.Status = "A";

                $("#approveLeave").modal('hide');
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        })
    }

    $scope.DeclineLeave = function () {
        if (vm.ModalLeave.DeclineReason === null || vm.ModalLeave.DeclineReason === "") {
            ErrorMessage("Reason is required");
        }
        else {
            $http({
                method: "POST",
                url: "/Leave/DeclineLeave",
                data: { leave: vm.ModalLeave }
            }).then(function (data) {
                if (data.data.errorMessage == "") {
                    SuccessMessage("Successfully declined leave")

                    vm.ModalLeave.Status = "D";

                    $("#declineLeave").modal('hide');
                }
                else {
                    ErrorMessage(data.data.errorMessage);
                }
            });
        }
    }
});