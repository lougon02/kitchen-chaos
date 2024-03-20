# Kitchen Chaos

### Notes

- [2:26:15] Used `transform.forward` instead of the movement direction. Fixes the issue, the code is shorter and the extra field isn't needed. I also changed the line that rotates the player to before the code that handles the movement, otherwise it won't rotate when stuck.

- [5:32:10] Using a state machine is unnecessarily complex. A much simpler solution is to just update the cached `fryingRecipeSO` with the recipe for the object that just finished cooking and do a null check. This is simpler, makes it easier to add larger chain recipes, and the extra `BurningRecipeSO` is not needed. To fire the `OnStateChanged` event, you have to do a check before changing the active recipe: The state changes if either the old or new recipe were null (XOR), which means it either started or finished cooking.

    ```cs
    private void ChangeActiveRecipe(FryingRecipeSO newFryingRecipeSO) {
        if((fryingRecipeSO == null) ^ (newFryingRecipeSO == null))
            // State Changed
            OnStateChanged?.Invoke(this, EventArgs.Empty);

        fryingRecipeSO = newFryingRecipeSO;
    }
    ```
