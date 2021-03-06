﻿var app = angular.module("app", ["angular-growl", "login", "HRModule", "calendar", "leave", "groupApprover", "calendar", "leaveType",
"AdvisoryModule"])

.controller("mainController", ['$scope', '$location', '$http', 'growl', function ($scope, $location, $http, growl) {
    var main = this;

    $scope.Init = function () {
        $http({
            method: "POST",
            url: "/Home/GetCurrentUser",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            main.CurrentUser = data.data.currentUser;
        });
    };

    $scope.Logout = function () {
        $http({
            method: "POST",
            url: "/Home/Logout",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data != "") {
                growl.error(data.data, { title: "Error!", ttl: 3000 });
            }
            else {
                window.location.href = "/Home/Login";
            }
        });
    };

    $scope.SaveEmail = function (value) {
        var ifError = "N";

        if (value.Email == "") {
            ifError == "Y";

            growl.error("Email is required", { title: "Error!", ttl: 3000 });
        }

        if (value.Password == "") {
            ifError = "Y";

            growl.error("Password is required", { title: "Error!", ttl: 3000 });
        }

        if (ifError == "N") {
            $http({
                method: "POST",
                url: "/Home/SaveEmail",
                data: { email: value }
            }).then(function (data) {
                if (data.data != "") {
                    growl.error(data.data, { title: "Error!", ttl: 3000 });
                }
                else {
                    growl.success("Successfully Saved", { ttl: 2000 });
                }
            });
        }
    };

    $scope.GetEmail = function () {
        $http({
            method: "POST",
            url: "/Home/GetEmail",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                growl.error(data.data, { title: "Error!", ttl: 3000 });
            }
            else {
                main.Account = data.data.email;
            }
        });
    };

    $scope.ClearPassModal = function(){
        vm.Pass = "";
    }

    $scope.ChangePassword = function(value){
        if(value.ConfirmPassword !== value.NewPassword){
            growl.error("Password Not Match", { title: "Error", ttl: 3000 });

            value.OldPassword = "";

            value.NewPassword = "";

            value.ConfirmPassword = "";
        }        

        $http({
            method: "POST",
            url: "/Home/ChangePassword",
            data: {
                oldPassword: value.OldPassword,
                newPassword: value.NewPassword}
            }).then(function(data){
            if(data.data !== ""){
                growl.error(data.data, { title: data.data, ttl: 3000 });
            }
            else{
                growl.success("Password successfully changed", { ttl: 2000 });

                $("#ChangePasswordModal").modal('hide');
            }
        });
    };
}]);