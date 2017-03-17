
var DoorTrackerApp = angular.module('DoorTrackerApp', ['ngRoute', 'ui.bootstrap', 'ngResource']).config(config);

config.$inject = ['$routeProvider', '$locationProvider', '$httpProvider'];

function config($routeProvider, $locationProvider, $httpProvider) {

	//$httpProvider.defaults.useXDomain = true;
	//delete $httpProvider.defaults.headers.common['X-Requested-With'];

	$routeProvider
	.when('/Home', {
		templateUrl: "scripts/spa/home/Index.html",
		controller: "homeCtrl"
	})
	 .when('/Door', {
		 templateUrl: "scripts/spa/door/Index.html",
		 controller: "doorCtrl"
	 })
	 .when('/AddDoor', {
		 templateUrl: "scripts/spa/door/registerDoor.html",
		 controller: "doorRegCtrl"
	 })
	 .when('/Customer', {
		 templateUrl: "scripts/spa/customer/Index.html",
		 controller: "customerCtrl"
	 })
	 .when('/AddCustomer', {
		 templateUrl: "scripts/spa/customer/registerCustomer.html",
		 controller: "customerRegCtrl"
	 })
	 .when('/Address', {
		 templateUrl: "scripts/spa/address/Index.html",
		 controller: "addressCtrl"
	 })
	 .when('/AddAddress', {
		  templateUrl: "scripts/spa/address/registerAddress.html",
		  controller: "addressRegCtrl"
	  })
	 .otherwise({ redirectTo: '/Home' });

	//$httpProvider.interceptors.push(function (Tester) {

	//    debugger;

	//    return {


	//        'request': function (config) {

	//            // if it's a request to the API, we need to provide the
	//            // access token as bearer token.             
	//            if (config.url.indexOf("http://localhost:55736/") === 0) {
	//                config.headers.Authorization = 'Bearer ' + Tester.token;
	//            }

	//            return config;
	//        }

	//    };
	//});


	if (window.history && window.history.pushState) {
		$locationProvider.html5Mode({
			enabled: true,
			requireBase: false
		});
	}
};


