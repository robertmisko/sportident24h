var app = angular.module("resultApp", ['ngResource', 'ngRoute']);

app.factory('Result', ['$resource', function ($resource) {
    var addr = 'http://localhost:8080';
    return {
        categories: $resource(addr + "/result/categories", {}, {
            query: { method: 'GET', isArray: true }
        }),

        courses: $resource(addr + "/result/courses", {}, {
            query: { method: 'GET', isArray: true }
        }),

        top: $resource(addr + "/result/categories/5", {}, {
            query: { method: 'GET', isArray: true }
        })
    };
}]);

app.controller("CatController", ['$scope', 'Result', function ($scope, Result) {
    Result.categories.query().$promise.then(function (res) {
        $scope.categories = res;
    });
}]);

app.controller("TopController", ['$scope', '$timeout', 'Result', function ($scope, $timeout, Result) {
    (function poll() {
        var cancelToken;
        Result.top.query().$promise.then(function (res) {
            $scope.row1 = [];
            $scope.row2 = [];
            $scope.row1 = res.splice(0, 2);
            if (res.length > 0) {
                $scope.row2 = res.splice(0, 2);
            }
            cancelToken = $timeout(poll, 10000);
        });

        $scope.$on('$destroy', function () { $timeout.cancel(cancelToken); })
    })();
}]);

app.controller("CourseController", ['$scope', 'Result', function ($scope, Result) {
    Result.courses.query().$promise.then(function (res) {
        $scope.courses = res;
    });
}]);

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
     .when('/', {
         templateUrl: 'Home/Categories',
         controller: 'CatController'
     })
    .when('/Course', {
        templateUrl: 'Home/Course',
        controller: 'CourseController'
    })
    .when('/Top', {
        templateUrl: 'Home/Top',
        controller: 'TopController'
    })
    .otherwise({
        redirectTo: '/'
    });

    //$locationProvider.html5Mode(true);
});