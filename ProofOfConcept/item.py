class Item:
    height: int
    width: int

    rotated: bool
    id: int

    def __init__(self, id, ht, wd) -> None:
        self.id = id
        self.height = ht
        self.width = wd

    def rotate(self):
        swap = self.height
        self.height = self.width
        self.width = swap