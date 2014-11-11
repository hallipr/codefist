function Player() {
    this.makeMove = function () {
        var move = Math.floor(Math.random() * 3);

        return ['rock', 'paper', 'scissors'][move];
    };
}