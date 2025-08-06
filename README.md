# Abandoned Euro-Style Solitaire Board-Game Project

An abandoned Unity project, my first attempt at a "bigger" game, mainly used to learn how to implement turn-based mechanics in a real-time environment and as a test ground for various mechanics and engine features and also board-game design.

## Game Idea
The main objective was to create a solitaire digital/physical Euro-Style Board Game with abstract strategy elements, inspired by Splotter Spellen games like The Great Zimbabwe with added asymetric player powers / tech-tree.

The challenge was supposed to come from an automated opponent that is implemented through simple decision system that does not use AI or complex flow-charts, rather using board-game Automata ideas, like priority cards, or even the player actions themsleves, in ways that "break" the rules but maintain a fair playing field.

The Wooden Disc-Stacks represent the player's and automated oponnent's corporations strength on the tile they are on, and in all iterations positioning on the board was key element.

As development progressed, the project became directionless, with the core mechanic transformed countless times. It went from a Bag-Building mechanic to a Dice-Selection mechanic, as I experimented to find what would work best. The final state was a pure abstract, relying on a future tech-tree and supply-demand mechanics, which resulted in an unbalanced and broken game.

As the mechanics became scattered, the codebase also became convoluted, coupled, and hard to maintain and expand. Instead of refactoring, I decided that I had learned enough from the project and should focus on games with solidly defined Core Mechanics and direction. These would allow me to finish development, make the game fun and balanced in a reasonable time.

This project was a very knowledgeable experience. Combined with extra materials, I learned how to do many things the "proper" way for future projects, including better design patterns for different situations, optimization techniques, and writing cleaner and more robust code, also some parts of the code I implemnted were used in other future projects due to their usefulness.

## Video Demonstration
Demonstrates the Placement and Capture mechanic. Disc placement is based on direct line to other discs of the same color, so if a new disc is placed with direct line to 3 of its color, a stack of height 3 will be added on that tile if empty. If an opponent stack is chosen, the game will allow to choose a taller stack that could capture the opponent's stack.

[![Video Demonstration](https://img.youtube.com/vi/eLgzE8-fEH0/0.jpg)](https://www.youtube.com/watch?v=eLgzE8-fEH0)

## Changes throughout development with GUI mockups for the current ruleset of the physical game

Initially, a token builder was used, where the player would use a hand of tokens pulled from a bag to perform different actions.

![Bag-Builder](ReadmeImages/EDBG_BagBuilder.png)

To simplify development and create a working prototype without being overwhelmed by designing a whole range of tokens and effects, I tried a simpler dice allocation game with unlockable power-ups.

![Dice Game](ReadmeImages/EDBG_Dice1.png)

After that, I experimented with implementing a system similar to Mike Lambo's Solitaire Wargame Books like Fields of Normandy, with the core mechanism revolving around dice allocation and action selection within each die. This offered more decision space while avoiding overwhelming the player with options. However, balancing it to make it fun, challenging, and with the correct ratio of Decision Space vs Randomized Die Rolls with modifiers proved to be more difficult than expected. As this was a training testground, this idea was ultimately scrapped.

![ML Style Dice Game](ReadmeImages/EDBG_Dice2.png)

In the last iteration, I attempted to streamline all the ideas into a more strategic game with less randomness, using a corporation-specific Tech-Tree. The tiles served as "Income" sources for demand tiles, requiring "sacrifices" to gather those resources based on Disc-Stack heights to supply the demand and unlock stronger abilities in the tech tree. However, it became clear that without redoing everything with a more concise direction and a codebase that assisted with the game instead of fighting against it, I decided to scrap the project as at that point I had better ideas for future projects that I could build faster from the ground-up and transform into a fully functional games without relying on the now obtuse codebase.

![Tech Tree and Demand](ReadmeImages/EDBG_TechTreeMockup.png)
