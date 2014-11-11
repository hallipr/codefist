(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.factory('matchService', [
    '$http', 'urls', function($http, urls) {
      return {
        queryByGame: function(gameId) {
          return $http.get(urls.matchesByGame(gameId));
        },
        queryByBot: function(gameId, botId) {
          return $http.get(urls.matchesByBot(gameId, botId));
        },
        create: function(gameId, botIds) {
          return $http.post(urls.matchesByGame(gameId), JSON.stringify(botIds));
        },
        get: function(matchId) {
          return $http.get(urls.match(matchId));
        }
      };
    }
  ]);

}).call(this);
