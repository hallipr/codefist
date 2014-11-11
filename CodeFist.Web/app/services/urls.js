(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.factory('urls', [
    '$http', function($http) {
      return {
        users: '/api/users',
        user: function(userId) {
          return "/api/users/" + userId;
        },
        games: '/api/games',
        gamesByUser: function(userId) {
          return "/api/users/" + userId + "/games";
        },
        game: function(gameId) {
          return "/api/games/" + gameId;
        },
        botsByGame: function(gameId) {
          return "/api/games/" + gameId + "/bots";
        },
        botsByUser: function(userId) {
          return "/api/users/" + userId + "/bots";
        },
        bot: function(gameId, botId) {
          return "/api/games/" + gameId + "/bots/" + botId;
        },
        matchesByGame: function(gameId) {
          return "/api/games/" + gameId + "/matches";
        },
        matchesByBot: function(gameId, botId) {
          return "/api/games/" + gameId + "/bots/" + botId + "/matches";
        },
        match: function(matchId) {
          return "/api/matches/" + matchId;
        },
        logout: '/security/logout',
        login: '/security/login',
        loginStatus: '/security/loginStatus'
      };
    }
  ]);

}).call(this);
