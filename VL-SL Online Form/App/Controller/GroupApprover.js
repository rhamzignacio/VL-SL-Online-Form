angular.module("groupApprover", [])

.controller("groupApproverController", function ($scope, $location, $http, growl) {
    var vm = this;

    vm.SelectedGroupName = "Select Group Approver";

    vm.SelectGroup = {};

    SuccessMessage = function (message) {
        growl.success(message, { ttl: 2000 });
    }

    ErrorMessage = function (message) {
        growl.error(message, { title: "Error!", ttl: 3000 });
    }

    GetGroupApprover = function () {
        $http({
            method: "POST",
            url: "/GroupApprover/GetAllGroup",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.data.errorMessage);
            }
            else {
                vm.Group = data.data.group;
            }
        });
    }

    GetUserDropdown = function () {
        $http({
            method: "POST",
            url: "/GroupApprover/GetUserDropDown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.data.errorMessage);
            }
            else {
                vm.UserDropdown = data.data.user;
            }
        });
    }

    $scope.InitGroupApprover = function(){
        GetGroupApprover();

        GetUserDropdown();
    }

    $scope.ClearModal = function () {
        vm.Modal = {};

        vm.Modal.FirstApprover = {};

        vm.Modal.SecondApprover = {};

        vm.Modal.Name = "";
    }

    $scope.GroupApproverModalClear = function () {
        vm.Modal = {};
    }

    $scope.GroupMemberModalClear = function () {
        vm.MemberModal = {};
    }

    $scope.AssignModal = function (value) {
        vm.Modal = value;
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

                vm.SelectGroup = data.data.group;

                vm.SelectedGroupName = data.data.group.Name;

                $("#SelectGroupModal").modal('hide');
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

                $("#GroupModal").modal('hide');

                GetGroupApprover();
            }
            else{
                ErrorMessage(data.data);
            }
        });
    }

    $scope.AssignGroupToDelete = function (value) {
        vm.ToBeDeleteGroup = value;
    }

    $scope.DeleteGroup = function () {
        $http({
            method: "POST",
            url: "/GroupApprover/DeleteGroupApprover",
            data: { group: vm.ToBeDeleteGroup }
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Successfully Deleted");

                $("#DeleteGroupModal").modal('hide');

                GetGroupApprover();
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }
});