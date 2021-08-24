from cell import Cell
from item import Item

import random

class Inventory:
    height: int
    width: int

    cells: list

    def __init__(self, ht, wd) -> None:
        self.height = ht
        self.width = wd
        self.cells = []

        for i in range(0, self.height * self.width):
            self.cells.append(Cell(None))

    def display(self) -> None:
        output = ""

        for i in range(0, self.width * self.height):
            output += "| " + self.cells[i].getItem() + " |"
            if i % self.width == self.width - 1:
                output += "\n"

        print(output)

    def populate(self) -> None:
        itemID = 0
        # While not full...
        while not self.isFull():
            # Get random fill point
            idx = random.randint(0, (self.height * self.width) - 1)
            if self.cells[idx].item != None:
                # If starting cell is not empty...
                continue
            else:
                # If starting cell is empty...
                # Generate random item of random size
                item = Item(itemID, random.randint(1, 3), random.randint(1, 5))

                # 50/50 chance to rotate item...
                if random.randint(0, 99) >= 50:
                    item.rotate()

                # Check if room for random item at starting point...
                if not self.roomAvailable(idx, item):
                    continue
                else:
                    itemID += 1

                    for i in range(0, item.width):
                        for j in range(0, item.height):
                            self.cells[idx + i + (j * self.width)].item = item
                    
                    self.display()

    def isFull(self) -> bool:
        for i in range(0, self.height * self.width):
            if self.cells[i].item == None:
                return False
        return True

    def roomAvailable(self, idx, item) -> bool:
        if ((idx % self.width) + (item.width - 1) >= self.width or int(idx / self.height) + (item.height - 1) >= self.height):
            return False
        
        for i in range(0, item.width):
            for j in range(0, item.height):
                if self.cells[idx + i + (j * self.width)].item != None:
                    return False

        return True