# https://judge.softuni.bg/Contests/Practice/Index/162#0

import math
PERCENT_LOST_OF_PAPER = float(9.8)
LOST_OF_PAPER = 1 + (PERCENT_LOST_OF_PAPER / 100)

packing_paper_quadrature = None
height = None
width = None
depth = None
try:
    packing_paper_quadrature = float(input())
    width = float(input())
    height = float(input())
    depth = float(input())
    surface_formula = 2 * ((width * height) + (width * depth) + (height * depth))
    surface_formula *= LOST_OF_PAPER
    print(math.ceil(surface_formula / packing_paper_quadrature))
except:
    print("INVALID INPUT")