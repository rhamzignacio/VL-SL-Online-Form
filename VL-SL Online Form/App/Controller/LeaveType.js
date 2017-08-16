angular.module("leaveType", [])

.controller("leaveTypeController", function ($scope, $location, $http, growl) {
    var vm = this;

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
});