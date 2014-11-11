codefist = angular.module 'codefist'

codefist.factory 'urls', ['$http', ($http) ->  
    users : '/api/users'
    user : (userId) -> "/api/users/#{userId}"
    
    games : '/api/games'
    gamesByUser : (userId) -> "/api/users/#{userId}/games"
    game : (gameId) -> "/api/games/#{gameId}"

    botsByGame : (gameId) -> "/api/games/#{gameId}/bots"
    botsByUser : (userId) -> "/api/users/#{userId}/bots"
    bot : (gameId, botId) -> "/api/games/#{gameId}/bots/#{botId}"

    matchesByGame : (gameId) -> "/api/games/#{gameId}/matches"
    matchesByBot : (gameId, botId) -> "/api/games/#{gameId}/bots/#{botId}/matches"
    match : (matchId) -> "/api/matches/#{matchId}"
   
    logout: '/security/logout'
    login: '/security/login'
    loginStatus: '/security/loginStatus'
]

