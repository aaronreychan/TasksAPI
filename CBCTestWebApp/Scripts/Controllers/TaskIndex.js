
var TaskIndex = function ($scope
    //, $uibModal
    ) {
    var vm = this;
    //depending on the permissions allow edit/delete
    vm.canAdd = false;
    vm.canGet = true;
    vm.canEdit = false;
    vm.canDelete = false;

    //hard coding for now
    vm.selectedPriorityType = '';
    vm.priorityTypes = [{ PriorityLookupId: 100, Name: 'High' },
                        { PriorityLookupId: 101, Name: 'Medium' },
                        { PriorityLookupId: 102, Name: 'Low' }]
    vm.tasks = [];
    vm.getTasks = function () {
        //if the application requires very strict permission checks, then the calls has to be done to local controller(s)
        //check permission there, if it's allowed from the backend call the external API
        //but since the application is our own, we can just reference the project and call the method(s) directly


        //depending on filters
        $.ajax({
            type: "get",
            url: "/tasks/getTasks",
            data: {
                PriorityLookupId: vm.selectedPriorityType
            }
        }).success(function (data) {
            vm.tasks = data;
            $scope.$digest();
        });
    }

    vm.Add = function() {
        var modalInstance = $uibModal.open({
            templateUrl: '~/Views/Tasks/Add.cshtml',
            controller: 'TaskAdd as vm',
            backdrop: 'static'
        });

        modalInstance.result.then(function () {
            vm.getTasks();
        });
    }

    vm.edit = function (Id) {
    }

    vm.delete = function (Id) {
        //call API
        //if success remove row
        //$("#taskId_" + Id).remove();
    }

    function init () {
        vm.getTasks();
    }

    init();
}

//Need to inject $uibModal
TaskIndex.$inject = ['$scope'];