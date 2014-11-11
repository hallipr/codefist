codefist = angular.module 'codefist'

codefist.factory 'matchService', ['$http', 'urls', ($http, urls) ->
    queryByGame: (gameId) -> $http.get(urls.matchesByGame(gameId))
    queryByBot: (gameId, botId) -> $http.get(urls.matchesByBot(gameId, botId))
    create: (gameId, botIds) -> $http.post(urls.matchesByGame(gameId), JSON.stringify(botIds))
    get: (matchId) -> $http.get(urls.match(matchId))
]