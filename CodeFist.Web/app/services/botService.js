(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.factory('botService', [
    '$http', 'urls', function($http, urls) {
      return {
        queryByGame: function(gameId) {
          return $http.get(urls.botsByGame(gameId));
        },
        queryByUser: function(userId) {
          return $http.get(urls.botsByUser(userId));
        },
        create: function(gameId, displayName) {
          return $http.post(urls.botsByGame(gameId), JSON.stringify(displayName));
        },
        get: function(gameId, botId) {
          return $http.get(urls.bot(gameId, botId));
        },
        "delete": function(gameId, botId) {
          return $http["delete"](urls.bot(gameId, botId));
        },
        update: function(bot) {
          return $http.put(urls.bot(bot.gameId, bot.botId), bot);
        }
      };
    }
  ]);

}).call(this);
