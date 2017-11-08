angular.module("HRModule", [])

.controller("hrModuleController", function ($scope, $location, $http, growl) {
    var vm = this;

    vm.SelectedUser = {};

    SuccessMessage = function (value) {
        growl.success("Successfully" + value, { ttl: 2000 });
    }

    ErrorMessage = function (value) {
        growl.error(value, { title: "Error!", ttl: 3000 });
    }

    $scope.UserTypeDropDown = [
        { value: "US", label: "User" },
        { value: "HR", label: "Human Resource" },
        { value: "ADM", label: "Admin" }
    ];

    //=======LEAVE CREDIT==========
    $scope.CreditLeavePerGroup = function (value) {
        $http({
            method: "POST",
            url: "/User/LeaveCreditPerGroup",
            data: {
                ID: value.ID,
                VL: value.VL,
                SL: value.SL
            }
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Successfully Credited Leave");

                $scope.SelectGroup();
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }

    $scope.CreditLeavePerEmp = function (value) {

        if (value.VL === "") {
            value.VL = 0;
        }

        if (value.SL === "") {
            value.SL = 0;
        }

        $http({
            method: "POST",
            url: "/User/LeaveCreditPerEmployee",
            data: {
                ID: value.ID,
                VL: value.VL,
                SL: value.SL
            }
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Successfully Credited Leave");

                $scope.SelectUser();
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }

    $scope.SelectGroup = function () {
        vm.Group.SL = '';

        vm.Group.VL = '';

        if (!(vm.Group.ID == null || vm.Group.ID == '')) {
            $http({
                method: "POST",
                url: "/User/GetGroupMember",
                data: { ID: vm.Group.ID }
            }).then(function (data) {
                if (data.data.errorMessage == "") {
                    vm.GroupMembers = data.data.users;
                }
                else {
                    ErrorMessage(data.data.errorMessage);
                }
            });
        }
    }
    $scope.SelectUser = function () {
        vm.Emp.SL = '';

        vm.Emp.VL = '';
        if (!(vm.Emp.ID == null || vm.Emp.ID == '')) {
            $http({
                method: "POST",
                url: "/User/GetSingleUser",
                data: { ID: vm.Emp.ID }
            }).then(function (data) {
                if (data.data.errorMessage == "") {
                    vm.SelectedUser = data.data.user;
                }
                else {
                    ErrorMessage(data.data.errorMessage);
                }
            });
        }
        else {
            vm.SelectedUser = {};
        }
    }

    $scope.InitLeaveCredit = function () {
        $http({
            method: "POST",
            url: "/User/GetUserDropdown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                vm.UserDropDown = data.data.user;
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });

        $http({
            method: "POST",
            url: "/GroupApprover/GetGroupApproverDropDown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                vm.GroupDropDown = data.data.group;
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
            });
    }

    //==========HR MODULE===============
    GetAllUsers = function () {
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

    $scope.Init = function () {
        GetAllUsers();

        $http({
            method: "POST",
            url: "/GroupApprover/GetGroupApproverDropDown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                vm.GroupDropDown = data.data.group;
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }


    $scope.ClearModal = function () {
        vm.Modal = {};
    }

    $scope.OpenModal = function (value) {
        vm.Modal = value;
    }

    $scope.AssignDeleteUser = function (value) {
        vm.UserToDelete = value;
    }

    $scope.DeleteUser = function () {
        $http({
            method: "POST",
            url: "/User/DeleteUser",
            data: { user: vm.UserToDelete }
        }).then(function (data) {
            if (data.data.message != "") {
                ErrorMessage(data.data.message);
            }
            else {
                SuccessMessage("Successfully deleted");

                $("#deleteModal").modal('hide');

                GetAllUsers();
            }
        });
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

                    GetAllUsers();
                }
            });
        }
    }
});