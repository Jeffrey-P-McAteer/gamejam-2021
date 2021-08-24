from item import Item

class Cell:
    item: Item

    def __init__(self, itm) -> None:
        self.item = itm

    def getItem(self):
        if self.item == None:
            return " "
        else:
            return str(self.item.id)