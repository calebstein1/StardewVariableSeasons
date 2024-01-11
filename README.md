# Stardew Variable Seasons

A mod for Stardew Valley that adds some variability to when the seasons actually change, rather than just changing on the first of each season.

Seasons will now change on a random day between 23 of the current season and 7 of the next season, weighted around 1 of the next season.
For example, the actual change from Spring to Summer can now occur on any day between Spring 23 and Summer 7.

About halfway through a season, the weather channel will give hints as to the length of the season, letting the player know whether the current season will be abnormally short, shorter than average, about average, longer than average, or abnormally long.

This mod will also include a mechanic to allow out-of-season crops to survive for some amount of time based on a percentage that decreases each day the crop remains out of season, and will allow for the purchase of crops for the next season from the general store.

This mod is still in very heavy development and is currently in an alpha state, I hope to have beta releases prepared by February 2024.

The mod currently runs, but you'll need to compile it yourself for the time being. There will be official binary releases once we hit the beta stage.

What works:
- Randomly selecting a date of next season change occurs during the night prior to day 15 each season
- The season progresses the night after the randomly selected number, rather than on the 1st of each season
- The weather channel will give a hint as to the length of the current season from days 16 through 20 each season

What's in development:
- Out-of-season crops will survive some amount of time past a season change before dying
- The general store will sell crop seeds for the next season as well as the current season

Known bugs:
- Festivals and birthdays in close proximity to the beginning or end of a season may happen twice or not at all depending on the random date of season change
- The year will increment on the randomly rolled date that Winter changes to Spring, rather than being fixed on Spring 1
- ??? (please report any you find)