using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async context =>
{
    var html = @"<!doctype html>
<html>
  <head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Rock Paper Scissors</title>
    <style>
      :root {
        --bg1: #0a0e27;
        --bg2: #141b2d;
        --accent1: #ff006e;
        --accent2: #8338ec;
        --accent3: #3a86ff;
        --success: #06d6a0;
        --danger: #fb5607;
        --text: #e6eef8;
        --card: rgba(255,255,255,0.03);
      }
      * { box-sizing: border-box; }
      html, body { height: 100%; margin: 0; }
      body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background: linear-gradient(135deg, var(--bg1), var(--bg2));
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 20px;
        color: var(--text);
        min-height: 100vh;
      }
      .container {
        max-width: 900px;
        width: 100%;
        background: linear-gradient(180deg, rgba(255,255,255,0.02), rgba(255,255,255,0.01));
        padding: 40px;
        border-radius: 20px;
        box-shadow: 0 20px 60px rgba(2,6,23,0.8), inset 0 1px 0 rgba(255,255,255,0.1);
        backdrop-filter: blur(10px);
        border: 1px solid rgba(255,255,255,0.05);
      }
      h1 {
        font-size: 2.5rem;
        margin: 0 0 10px;
        background: linear-gradient(90deg, var(--accent1), var(--accent2), var(--accent3));
        -webkit-background-clip: text;
        background-clip: text;
        color: transparent;
        text-align: center;
        font-weight: 700;
      }
      .subtitle {
        text-align: center;
        opacity: 0.8;
        margin-bottom: 40px;
        font-size: 1rem;
      }
      .game-area {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 30px;
        margin-bottom: 40px;
      }
      .player-section {
        display: flex;
        flex-direction: column;
        align-items: center;
      }
      .player-label {
        font-size: 0.9rem;
        text-transform: uppercase;
        letter-spacing: 2px;
        opacity: 0.7;
        margin-bottom: 15px;
      }
      .choice-display {
        font-size: 4rem;
        margin: 15px 0;
        min-height: 100px;
        display: flex;
        align-items: center;
        justify-content: center;
      }
      .buttons-grid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 15px;
        width: 100%;
        margin-bottom: 20px;
      }
      button {
        padding: 20px;
        border: 2px solid rgba(255,255,255,0.1);
        border-radius: 12px;
        background: linear-gradient(135deg, rgba(255,255,255,0.05), rgba(255,255,255,0.02));
        color: var(--text);
        font-size: 1.2rem;
        cursor: pointer;
        transition: all 0.3s ease;
        font-weight: 600;
        position: relative;
        overflow: hidden;
      }
      button:hover {
        border-color: rgba(255,255,255,0.2);
        background: linear-gradient(135deg, rgba(255,255,255,0.1), rgba(255,255,255,0.05));
        transform: translateY(-2px);
        box-shadow: 0 8px 16px rgba(255,255,255,0.1);
      }
      button:active {
        transform: translateY(0);
      }
      .result-section {
        text-align: center;
        background: rgba(255,255,255,0.02);
        padding: 30px;
        border-radius: 12px;
        border: 1px solid rgba(255,255,255,0.05);
        margin-bottom: 30px;
      }
      .result-text {
        font-size: 1.8rem;
        font-weight: 700;
        margin-bottom: 10px;
        min-height: 40px;
      }
      .result-text.win {
        color: var(--success);
      }
      .result-text.lose {
        color: var(--danger);
      }
      .result-text.draw {
        color: var(--accent2);
      }
      .score-board {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 15px;
        margin-bottom: 20px;
      }
      .score-box {
        background: linear-gradient(135deg, rgba(255,255,255,0.05), rgba(255,255,255,0.02));
        padding: 20px;
        border-radius: 10px;
        border: 1px solid rgba(255,255,255,0.05);
      }
      .score-label {
        font-size: 0.8rem;
        text-transform: uppercase;
        opacity: 0.7;
        margin-bottom: 8px;
        letter-spacing: 1px;
      }
      .score-value {
        font-size: 2rem;
        font-weight: 700;
        background: linear-gradient(90deg, var(--accent1), var(--accent2));
        -webkit-background-clip: text;
        background-clip: text;
        color: transparent;
      }
      .reset-btn {
        width: 100%;
        padding: 15px;
        background: linear-gradient(90deg, var(--accent1), var(--accent2));
        border: none;
        color: white;
        font-weight: 700;
        border-radius: 12px;
        cursor: pointer;
        font-size: 1rem;
        transition: all 0.3s ease;
      }
      .reset-btn:hover {
        transform: scale(1.05);
        box-shadow: 0 10px 30px rgba(255, 0, 110, 0.4);
      }
      @media (max-width: 768px) {
        h1 { font-size: 1.8rem; }
        .game-area { grid-template-columns: 1fr; }
        .choice-display { font-size: 3rem; }
      }
    </style>
  </head>
  <body>
    <div class=""container"">
      <h1>🎮 Rock Paper Scissors</h1>
      <p class=""subtitle"">Play against the computer</p>
      
      <div class=""game-area"">
        <div class=""player-section"">
          <div class=""player-label"">Your Choice</div>
          <div class=""choice-display"" id=""playerChoice"">?</div>
          <div class=""buttons-grid"">
            <button onclick=""playGame('rock')"">🪨<br>Rock</button>
            <button onclick=""playGame('paper')"">📄<br>Paper</button>
            <button onclick=""playGame('scissors')"">✂️<br>Scissors</button>
          </div>
        </div>
        
        <div class=""player-section"">
          <div class=""player-label"">Computer Choice</div>
          <div class=""choice-display"" id=""computerChoice"">?</div>
          <div style=""opacity: 0.5; text-align: center; font-size: 0.9rem;"">Thinking...</div>
        </div>
      </div>
      
      <div class=""result-section"">
        <div class=""result-text"" id=""resultText"">Make your move!</div>
      </div>
      
      <div class=""score-board"">
        <div class=""score-box"">
          <div class=""score-label"">Your Wins</div>
          <div class=""score-value"" id=""playerWins"">0</div>
        </div>
        <div class=""score-box"">
          <div class=""score-label"">Draws</div>
          <div class=""score-value"" id=""draws"">0</div>
        </div>
        <div class=""score-box"">
          <div class=""score-label"">Computer Wins</div>
          <div class=""score-value"" id=""computerWins"">0</div>
        </div>
      </div>
      
      <button class=""reset-btn"" onclick=""resetGame()"">Reset Game</button>
    </div>

    <script>
      const choices = ['rock', 'paper', 'scissors'];
      const emojis = { rock: '🪨', paper: '📄', scissors: '✂️' };
      let stats = { playerWins: 0, draws: 0, computerWins: 0 };

      function getComputerChoice() {
        return choices[Math.floor(Math.random() * choices.length)];
      }

      function determineWinner(player, computer) {
        if (player === computer) return 'draw';
        if ((player === 'rock' && computer === 'scissors') ||
            (player === 'paper' && computer === 'rock') ||
            (player === 'scissors' && computer === 'paper')) {
          return 'win';
        }
        return 'lose';
      }

      function playGame(playerChoice) {
        const computerChoice = getComputerChoice();
        const result = determineWinner(playerChoice, computerChoice);

        document.getElementById('playerChoice').textContent = emojis[playerChoice];
        document.getElementById('computerChoice').textContent = emojis[computerChoice];

        const resultText = document.getElementById('resultText');
        if (result === 'win') {
          stats.playerWins++;
          resultText.textContent = '🎉 You Win!';
          resultText.className = 'result-text win';
        } else if (result === 'lose') {
          stats.computerWins++;
          resultText.textContent = '🤖 Computer Wins!';
          resultText.className = 'result-text lose';
        } else {
          stats.draws++;
          resultText.textContent = '🤝 Draw!';
          resultText.className = 'result-text draw';
        }

        document.getElementById('playerWins').textContent = stats.playerWins;
        document.getElementById('draws').textContent = stats.draws;
        document.getElementById('computerWins').textContent = stats.computerWins;
      }

      function resetGame() {
        stats = { playerWins: 0, draws: 0, computerWins: 0 };
        document.getElementById('playerChoice').textContent = '?';
        document.getElementById('computerChoice').textContent = '?';
        document.getElementById('resultText').textContent = 'Make your move!';
        document.getElementById('resultText').className = 'result-text';
        document.getElementById('playerWins').textContent = '0';
        document.getElementById('draws').textContent = '0';
        document.getElementById('computerWins').textContent = '0';
      }
    </script>
  </body>
</html>";

    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(html);
});

app.Run();
