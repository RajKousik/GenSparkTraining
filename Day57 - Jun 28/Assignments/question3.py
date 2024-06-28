def sort_players_by_score(players: dict) -> list:
    """
    Sort players by their scores in descending order.

    :param players: Dictionary of player names and their scores
    :return: List of tuples with player names and scores, sorted by scores
    """
    try:
        sorted_players = sorted(players.items(), key=lambda x: x[1], reverse=True)
        return sorted_players[:10]
    except Exception as e:
        print(f"An error occurred: {e}")
        return []


player_scores = {
    "Alice": 85,
    "Bob": 95,
    "Charlie": 90,
    "David": 88,
    "Eve": 92,
    "Frank": 77,
    "Grace": 82,
    "Heidi": 89,
    "Ivan": 91,
    "Judy": 94,
    "Karl": 80,
    "Liam": 86
}
top_players = sort_players_by_score(player_scores)
print("Top 10 players:")
for player, score in top_players:
    print(f"{player}: {score}")
