angular.module("groupApprover", [])

.controller("groupApproverController", function ($scope, $location, $http, $growl) {
    var vm = this;

    SuccessMessage = function (message) {
        growl.success(message, { ttl: 2000 });
    }

    ErrorMessage = function (message) {
        growl.error(message, { title: "Error!", ttl: 3000 });
    }

    $scope.InitGroupApprover = function(){
        $http({
            method: "POST",
            url: "/GroupApprover/GetGroupApproverDropDown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.data.errorMessage);
            }
            else {
                vm.GroupApproverDropDown = data.data.Group;
            }
        })
    }

    $scope.SelectGroup = function () {
        $http({
            method: "POST",
            url: "/GroupApprover/GetGroupMember",
            data: { ID: vm.SelectedGroup }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.data.errorMessage);
            }
            else {
                vm.GroupMember = data.data.member;

                $("#GroupModal").modal('hide');
            }
        });
    }

    $scope.SaveGroup = function(value){
        $http({
            method: "POST",
            url: "/GroupApprover/SaveGroupApprover",
            data: { group: value }
        }).then(function(data){
            if(data.data === ""){
                SuccessMessage("Successfully Saved");
            }
            else{
                ErrorMessage(data.data);
            }
        });
    }

    $scope.SaveMember = function (value) {
        $http({
            method: "POST",
            url: "GroupApprover/SaveGroupMember",
            data: { member: value }
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Successfully Saved");
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }
});