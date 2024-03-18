# Kitchen Chaos

### Notes

- [2:26:15] Used `transform.forward` instead of the movement direction. Fixes the issue, the code is shorter and the extra field isn't needed. I also changed the line that rotates the player to before the code that handles the movement, this way `transform.forward` isn't affected by the changes in the move direction vector, effectively looking in the direction in which the player wants to move, instead of the direction it is moving. This makes it easier for the player to interact with a counter when they move while stuck to the wall.