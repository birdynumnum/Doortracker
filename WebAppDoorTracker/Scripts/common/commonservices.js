(function () {
	"use strict";

	angular.module("commonservices",
					["ngResource"])
		.constant("appSettings",
		{
			DoorTrackerAPI: "https://localhost:55736"
		});


}());