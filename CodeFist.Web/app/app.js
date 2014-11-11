(function() {
  var codefist;



  codefist = angular.module('codefist', ['ngRoute', 'ui.codemirror', 'ui.bootstrap']);

  codefist.config([
    '$routeProvider', function($routeProvider) {
      return $routeProvider.when('/games/', {
        templateUrl: '/app/views/game-list.html',
        controller: 'GameListCtrl'
      }).when('/games/createGame', {
        templateUrl: '/app/views/game-detail.html',
        controller: 'GameDetailCtrl',
        creatingGame: true
      }).when('/games/:gameId', {
        templateUrl: '/app/views/game-detail.html',
        controller: 'GameDetailCtrl',
        creatingGame: false
      }).when('/games/:gameId/bots/:botId', {
        templateUrl: '/app/views/bot-detail.html',
        controller: 'BotDetailCtrl',
        creatingBot: false
      }).when('/games/:gameId/createBot', {
        templateUrl: '/app/views/bot-detail.html',
        controller: 'BotDetailCtrl',
        creatingBot: true
      }).when('/games/:gameId/fight/:botIds', {
        templateUrl: '/app/views/fight-detail.html',
        controller: 'LocalFightCtrl'
      }).when('/matches/:matchId', {
        templateUrl: '/app/views/fight-detail.html',
        controller: 'MatchReplayCtrl'
      }).when('/users/', {
        templateUrl: '/app/views/user-list.html',
        controller: 'UserListCtrl'
      }).when('/users/:userId', {
        templateUrl: '/app/views/user-detail.html',
        controller: 'UserDetailCtrl'
      }).otherwise({
        redirectTo: '/games'
      });
    }
  ]);

  codefist.directive('contenteditable', function() {
    return {
      require: 'ngModel',
      link: function(scope, element, attrs, ctrl) {
        var replaceSelection;
        element.bind('blur', function() {
          return scope.$apply(function() {
            return ctrl.$setViewValue(element.html());
          });
        });
        replaceSelection = function(replacement) {
          var cursorPosition, end, node, original, range, sel, start;
          sel = window.getSelection();
          if (sel.rangeCount === 0) {
            return;
          }
          range = sel.getRangeAt(0);
          original = element.html();
          start = original.substring(0, range.startOffset);
          end = original.substring(range.endOffset);
          cursorPosition = range.startOffset + replacement.length;
          element.html(start + replacement + end);
          range = document.createRange();
          node = element[0].firstChild;
          range.setStart(node, cursorPosition);
          range.setEnd(node, cursorPosition);
          sel.removeAllRanges();
          return sel.addRange(range);
        };
        element.bind('keydown', function(event) {
          switch (event.keyCode) {
            case 13:
              replaceSelection('\n');
              return event.preventDefault();
            case 9:
              replaceSelection('    ');
              return event.preventDefault();
          }
        });
        ctrl.$render = function() {
          return element.html(ctrl.$viewValue);
        };
        return ctrl.$render();
      }
    };
  });

}).call(this);
