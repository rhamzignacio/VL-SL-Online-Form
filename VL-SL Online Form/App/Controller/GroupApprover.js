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
            url: "/GroupApprover/GetGroupApproverDropDown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.data.errorMessage);
            }
            else {
                vm.GroupApproverDropDown = data.data.group;
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

    $scope.GroupApproverModalClear = function () {
        vm.Modal = {};
    }

    $scope.GroupMemberModalClear = function () {
        vm.MemberModal = {};
    }

    $scope.AssignModal = function () {
        vm.Modal = vm.SelectGroup;
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

    $scope.SaveMember = function (value) {
        value.GroupID = vm.SelectedGroup;

        $http({
            method: "POST",
            url: "/GroupApprover/SaveGroupMember",
            data: { member: value }
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Successfully Saved");

                $("#GroupModal").modal('hide');

                $scope.SelectGroup();
            }
            else {
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
            data: { group: vm.SelectGroup }
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Successfully Deleted");

                vm.SelectedGroupName = "Select Group Approver";

                vm.GroupMember = {};

                $("#DeleteGroupModal").modal('hide');
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }

    $scope.AssignMemberToDelete = function (value) {
        vm.ToBeDeleteMember = value;
    }

    $scope.DeleteMember = function () {
        $http({
            method: "POST",
            url: "/GroupApprover/DeleteGroupMember",
            data: { member: vm.ToBeDeleteMember}
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Successfully Deleted");

                $scope.SelectGroup();

                $("#DeleteMemberModal").modal('hide');
            }
            else {
                ErrorMessage(data.data);
            }
        })
    }
});