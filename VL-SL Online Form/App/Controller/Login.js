angular.module("login", ["angular-growl"])

.controller("loginController", ['$scope', '$location', '$http', 'growl', function ($scope, $location, $http, growl) {
    var vm = this;

    $scope.TryLogin = function (username, password) {
        $http({
            method: "POST",
            url: "/Home/Login",
            data: {
                username: username,
                password: password
            }
        }).then(function (data) {
            if (data.data != "") {
                growl.error(data.data, { ttl: 3000 });
            }
            else {
                window.location.href = "/Leave/FiledForms";
            }
        })
    }
}])