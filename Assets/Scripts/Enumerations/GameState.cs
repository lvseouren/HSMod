// WARNING : Probably won't need gamestates anyways, but we'll keep them for now

/* Active state of the game
 * MULLIGAN happens once at the start of match
 * START is when a specific player's turn start
 * ACTIVE happens between START and END
 * END is when a specific player's turn ends
 */
public enum GameState
{
    Mulligan,
    Start,
    End,
    Active
}