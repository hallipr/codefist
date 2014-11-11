(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.factory('gameService', [
    '$http', 'urls', function($http, urls) {
      return {
        query: function() {
          return $http.get(urls.games);
        },
        gamesByUser: function(userId) {
          return $http.get(urls.gamesByUser(userId));
        },
        create: function(displayName) {
          return $http.post(urls.games, JSON.stringify(displayName));
        },
        get: function(id) {
          return $http.get(urls.game(id));
        },
        "delete": function(id) {
          return $http["delete"](urls.game(id));
        },
        update: function(game) {
          return $http.put(urls.game(game.id), game);
        }
      };
    }
  ]);

}).call(this);
