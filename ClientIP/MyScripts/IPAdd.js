(function () {
    //create angularjs controller
    var app = angular.module('IPapp', []);//set and get the angular module
    app.controller('IPController', ['$scope', '$http', IPController]);

    //angularjs controller method
    function IPController($scope, $http) {
        alert("vatchindi");
        //get all customer information
        $http.get('/api/IPAddressAPI/').success(function (data) {
            $scope.IPAdds = data;
        })
        .error(function () {
            $scope.error = "An Error has occured while loading posts!";
        });
    }
})();
