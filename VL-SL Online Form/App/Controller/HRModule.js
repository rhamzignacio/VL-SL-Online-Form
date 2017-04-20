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
            url: "/User/Init",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.message == "") {
                vm.Users = data.data.users;

                vm.UserDropdown = data.data.userDropdown;
            }
            else {
                ErrorMessage(data.data.message);
            }
        });
    }

    $scope.ClearModal = function () {
        vm.Modal = {};
    }

    $scope.OpenModal = function (value) {
        vm.Modal = value;
    }

    $scope.DeleteUser = function () {
        vm.Modal.Status = "X";
    }

    $scope.SaveUser = function (value) {
        if (value.Password != value.ConfirmPassword) {
            ErrorMessage("Password Not Match");

            value.Password = "";

            value.ConfirmPassword = "";
        }
        else {
            $http({
                method: "POST",
                url: "/User/SaveUser",
                data: { user: value }
            }).then(function (data) {
                if (data.data.message != "") {
                    ErrorMessage(data.data.message);
                }
                else {
                    SuccessMessage("Saved");

                    $("#userModal").modal('hide');
                }
            });
        }
    }
});