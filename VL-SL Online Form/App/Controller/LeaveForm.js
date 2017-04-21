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

    $scope.Save = function (value) {
        $http({
            method: "POST",
            url: "/Leave/SaveLeave",
            data: { leave: value }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                SuccessMessage("Successfully Saved");
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }
});