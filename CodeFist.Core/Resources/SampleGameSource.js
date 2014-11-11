function Game() {
    this.play = function (players, done) {
        players.forEach(function (player) {
            player.instance = new player.constructor();
        });

        // Play the game
        var betterMoves = {
            rock: "paper",
            paper: "scissors",
            scissors: "rock"
        };

        var history = [],
            scores = [0, 0];
        
        for (var round = 0; round < 1000; round++) {
            var moves = players.map(function (player) { return player.instance.makeMove(); });

            var winner = moves[0] == moves[1] ? null
                : betterMoves[moves[1]].indexOf(moves[0]) == -1 ? 0 : 1;

            if (winner != null) {
                scores[winner] += 1;
            }
            
            history.push({ moves: moves, winner: winner });
        }

        done({
            history: history,
            winner: scores[0] === scores[1] ? null
                : scores[0] > scores[1] ? 0 : 1
        });
    };
}