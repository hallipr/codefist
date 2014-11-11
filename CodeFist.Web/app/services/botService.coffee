codefist = angular.module 'codefist'

codefist.factory 'botService', ['$http', 'urls', ($http, urls) ->
    queryByGame: (gameId) -> $http.get(urls.botsByGame(gameId))
    queryByUser: (userId) -> $http.get(urls.botsByUser(userId))
    create: (gameId, displayName) -> $http.post(urls.botsByGame(gameId), JSON.stringify(displayName))
    get: (gameId, botId) -> $http.get(urls.bot(gameId, botId))
    delete: (gameId, botId) -> $http.delete(urls.bot(gameId, botId))
    update: (bot) -> $http.put(urls.bot(bot.gameId, bot.botId), bot)
]