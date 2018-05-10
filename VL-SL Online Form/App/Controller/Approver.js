angular.module("AdvisoryModule", [])

.controller("AdvisoryController", function($scope, $location, $http, growl){
    var vm = this;

    
    SuccessMessage = function (value) {
        growl.success("Successfully" + value, { ttl: 2000 });
    };

    ErrorMessage = function (value) {
        growl.error(value, { title: "Error!", ttl: 3000 });
    };

    $scope.Init = function(){
        $http({
            method: "POST",
            url: "/Advisory/GetAll",
            arguments: { "Content-Type": "application/json" } 
        }).then(function(data){
            if(data.data.message !== ""){
                ErrorMessage(data.data.message);
            }
            else{
                vm.Advisory = data.data.advisory;
            }
        });
    };

    $scope.SaveAdvisory = function(value){
        if(value.Description === ""){
            ErrorMessage("Advisory is required");
        }
        else{
            $http({
                method: "POST",
                url: "/Advisory/SaveAdvisory",
                data: {advisory: value}
            }).then(function(data){
                if(data.data !== "" ){
                    ErrorMessage(data.data);
                }
                else{
                    SuccessMessage("Successfully Saved");

                    $scope.Init();

                    $scope.ClearModal();

                    $("#AdvisoryModal").modal('hide');

                    $("#DeleteAdvisoryModal").modal('hide');
                }
            });
        }
    };

    $scope.DeleteAdvisory = function () {
        vm.Delete.Status = 0;

        $scope.SaveAdvisory(vm.Delete);
    };

    $scope.AssignModal = function (value) {
        vm.Modal = value;
    };

    $scope.AssignDelete = function (value) {
        vm.Delete = value;
    };

    $scope.ClearModal = function () {
        vm.Modal = {};
    };
});