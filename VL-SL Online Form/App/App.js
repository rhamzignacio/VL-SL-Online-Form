var app = angular.module("app",["angular-growl", "login", "HRModule", "calendar"])

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
    }

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
    }
}]);