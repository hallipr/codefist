(function() {
  var codefist,
    __slice = [].slice;



  codefist = angular.module('codefist');

  codefist.factory('session', [
    '$http', '$rootScope', '$window', 'urls', function($http, $rootScope, $window, urls) {
      var session;
      session = {
        user: {},
        isLoggedIn: function() {
          return !!this.user.enabled;
        },
        isLoading: function() {
          return !!this.request || !!this.loginWindow;
        },
        login: function(width, height) {
          var left, top, windowParams;
          if (width == null) {
            width = 1000;
          }
          if (height == null) {
            height = 650;
          }
          height = Math.min(height, screen.height);
          width = Math.min(width, screen.width);
          left = screen.width > width ? Math.round((screen.width / 2) - (width / 2)) : 0;
          top = screen.height > height ? Math.round((screen.height / 2) - (height / 2)) : 0;
          windowParams = "left=" + left + ",top=" + top + ",width=" + width + ",height=" + height + ",personalbar=0,toolbar=0,scrollbars=1,resizable=1";
          this.loginWindow = window.open(urls.login, 'Sign in with Github', windowParams);
          if (this.loginWindow) {
            return this.loginWindow.focus();
          }
        },
        logout: function() {
          var self;
          self = this;
          return $http({
            url: urls.logout
          }).then(function() {
            return self.processLoginResult({
              success: false
            });
          })["catch"](function() {
            return console.error.apply(console, ['Error logging out'].concat(__slice.call(arguments)));
          });
        },
        processLoginResult: function(result) {
          if (result.success) {
            return this.user = {
              name: result.userDisplayName,
              id: result.userId,
              enabled: result.enabled
            };
          } else {
            return this.user = {};
          }
        },
        checkLoginStatus: function() {
          var self;
          self = this;
          return $http({
            url: urls.loginStatus,
            method: 'POST'
          }).then(function(response) {
            return self.processLoginResult(response.data);
          });
        },
        initializeScope: function(scope) {
          var self;
          self = this;
          scope.isLoggedIn = function() {
            return self.isLoggedIn();
          };
          scope.isLoading = function() {
            return self.isLoading();
          };
          return scope.username = function() {
            return self.user.name;
          };
        }
      };
      $window._handleLoginResponse = function(result) {
        $rootScope.$apply(function() {
          return session.processLoginResult(result);
        });
        return delete session.loginWindow;
      };
      session.checkLoginStatus();
      setInterval(session.checkLoginStatus.bind(session), 1000 * 60 * 5);
      return session;
    }
  ]);

}).call(this);
