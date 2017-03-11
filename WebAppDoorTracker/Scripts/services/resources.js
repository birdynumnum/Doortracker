DoorTrackerApp.factory('resources', resources);

resources.$inject = ['$resource', 'tokenContainer'];

function resources($resource) {

    var tst = tokenContainer.tokenok;


    return $resource('http://localhost://55736', null,
               {
                   'query':
                       {
                           isArray: true,
                           headers: { 'Authorization': 'Bearer' + tokenContainer.tokenok }
                       },

               });
}


