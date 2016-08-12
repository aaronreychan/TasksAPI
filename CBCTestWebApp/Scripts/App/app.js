var myAngularApp = angular.module('myAngularApp', ['ngRoute', 'ui.bootstrap']);

//var configFunction = function ($routeProvider) {
//    debugger;
//    $routeProvider.
//        when('/tasks', {
//            templateUrl: 'http://localhost:50192/tasks/getTasks'
//        })
//}
//configFunction.$inject = ['$routeProvider'];

//myAngularApp.config(configFunction);

myAngularApp.filter("jsDate", function () {
    return function (x) {
        return new Date(parseInt(x.substr(6)));
    };
});