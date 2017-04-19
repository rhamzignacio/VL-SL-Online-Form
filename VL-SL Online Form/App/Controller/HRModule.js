angular.module("HRModule", [])

.controller("hrModuleController", function ($scope, $location, $http, growl) {
    var vm = this;

    SuccessMessage = function (value) {
        growl.success("Successfully" + value, { ttl: 2000 });
    }

    ErrorMessage = function (value) {
        growl.error(value, { title: "Error!", ttl: 3000 });
    }

    $scope.DepartmentDropdown = [
        { value: "Information Technology", label: "Information Technology (IT)" },
        { value: "Accounting", label: "Accounting (ACCTG)" },
        { value: "Human Resource", label: "Human Resource (HR)" },
    ];

    $scope.Init = function () {
        $http({
            method: "POST",
            url: "/User/GetAllUser",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.message == "") {
                vm.Users = data.data.users;
            }
            else {
                ErrorMessage(data.data.message);
            }
        });
    }

    $scope.SaveUser = function (value) {
        $http({
            method: "POST",
            url: "/User/SaveUser",
            data: { user: value }
        }).then(function(data){
            if (data.data.message != "") {
                ErrorMessage(data.data.message);
            }
            else {
                SuccessMessage("Saved");
            }
        });
    }
});