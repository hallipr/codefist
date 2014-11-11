codefist = angular.module 'codefist'

codefist.factory 'session', ['$http', '$rootScope', '$window', 'urls', ($http, $rootScope, $window, urls) ->
    session =
        user: {}

        isLoggedIn: -> !!@user.enabled

        isLoading: -> !!@request or !!@loginWindow
        
        login: (width = 1000, height = 650) ->
            height = Math.min(height, screen.height)
            width = Math.min(width, screen.width)
            left = if (screen.width > width) then Math.round((screen.width / 2) - (width / 2)) else 0
            top = if (screen.height > height) then Math.round((screen.height / 2) - (height / 2)) else 0
        
            windowParams = "left=#{left},top=#{top},width=#{width},height=#{height},personalbar=0,toolbar=0,scrollbars=1,resizable=1"
        
            @loginWindow = window.open(urls.login, 'Sign in with Github', windowParams)

            if @loginWindow then @loginWindow.focus()
    
        logout: ->
            self = @
      
            $http(url: urls.logout)
                .then( -> self.processLoginResult(success: false))
                .catch( -> console.error 'Error logging out', arguments...)

        processLoginResult: (result) -> 
            if result.success
            then @user = 
                    name: result.userDisplayName
                    id: result.userId
                    enabled: result.enabled
            else @user = {}

        checkLoginStatus: ->
            self = @

            $http(url: urls.loginStatus, method: 'POST')
                .then((response) -> self.processLoginResult(response.data))

        initializeScope: (scope) -> 
            self = @

            scope.isLoggedIn = -> self.isLoggedIn()
            scope.isLoading = -> self.isLoading()
            scope.username = -> self.user.name

    $window._handleLoginResponse = (result) ->
        $rootScope.$apply -> session.processLoginResult result
        
        delete session.loginWindow

    session.checkLoginStatus()

    setInterval(session.checkLoginStatus.bind(session), 1000 * 60 * 5)

    return session;
]