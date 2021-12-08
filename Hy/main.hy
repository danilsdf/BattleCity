(import [random[randint]]
[copy[copy]])

(setv userX 0) (setv userY 2)
(setv enemyX 3) (setv enemyY 2)
(setv arraySize 5)

(setv labirintArray [])
(setv userPoint [userX userY])
(setv enemyPoint [enemyX enemyY])

; Generate our walls
(setv walls [])

(for [x (range arraySize)](
  do
  (setv points [])
  (points.append (randint 0 (- arraySize 1)))
  (points.append (randint 0 (- arraySize 1)))
  (walls.append points)
))

(setv apples [])

(defn isEqual[point, secondpoint](
	do
	(return (and (= (get point 0) (get secondpoint 0)) (= (get point 1) (get secondpoint 1))))
))

; (print walls)

(for [x (range arraySize)](
  do
  (setv tempArray [])
  (for [y (range arraySize)](
    do
    (setv point [])
    (point.append x) (point.append y)
    (setv isUser (isEqual point userPoint))
    (setv isEnemy (isEqual point enemyPoint))
    (cond
     [(and (in point walls) (and (not isUser) (not isEnemy)))
      (tempArray.append 1)]
     [isUser
      (tempArray.append 2)]
     [isEnemy
      (tempArray.append 3)]
     [True
  	  (tempArray.append 0)(apples.append point)])
  ))
  (labirintArray.append tempArray)
))

(for [x (range arraySize)](
  do
	(print (get labirintArray x))
))

(defn getNeighbors[point [neighborParent None]](
  do
  (setv neighbors [[0 1] [0 -1] [1 0] [-1 0]])
  (setv availbleNeighbors [])
  (for [neighbor neighbors](
    do
    (setv x (get neighbor 0))
    (setv y (get neighbor 1))
    (setv neighbor [])
    (neighbor.append (%(+(get point 0) x)5))
    (neighbor.append (%(+(get point 1) y)5))
    (if (is neighborParent None)
    (neighbor.append (copy neighbor))
    (neighbor.append neighborParent)
    )
    (if (!= 1 (get labirintArray (get neighbor 0) (get neighbor 1)))
    (availbleNeighbors.append neighbor))
  ))
  availbleNeighbors
))

; (print (getNeighbors [3 2]))

(defn makeEnemyTurn[enemyPoint userPoint](
  do
  (setv userPointX (get userPoint 0))
  (setv userPointY (get userPoint 1))
  (setv visited [])
  (visited.append enemyPoint)
  (setv nextNodesToVisit (getNeighbors enemyPoint))
  (while (!= 0 (len nextNodesToVisit))(
    do
    (setv nodesToVisit nextNodesToVisit)
    (setv nextNodesToVisit [])
    (for [node nodesToVisit](
      do
      (setv node_x (get node 0))
      (setv node_y (get node 1))
      (setv node_without_neighbor [])
      (node_without_neighbor.append node_x)
      (node_without_neighbor.append node_y)
      (if (not (in node_without_neighbor visited))(
        do
        (visited.append node_without_neighbor)
        (if (and (= userPointX node_x  (= userPointY node_y)))
        (return (get node 2))
        )
        (+= nextNodesToVisit (getNeighbors node (get node 2)))
      ))
    ))
  ))
  (get (getNeighbors enemyPoint) 0)
))

(defn pacmanAction[](
  maxTurn 2 userPoint enemyPoint 0
))

(defn maxTurn[deep userPoint enemyPoint score](
  do
  (if (or (= deep 0)(= 0 (len apples)))(
    return score
  ))
  (setv availbleNeighbors (getNeighbors userPoint))
  (setv scores [])
  (for [neighbor availbleNeighbors](
    do
    (setv short_neighbor (cut neighbor 0 2))
    (setv temp_score score)
    (setv removed_apple None)
    (if (in short_neighbor apples)(
      do
      (setv temp_score (+ temp_score 1))
      (apples.remove short_neighbor)
      (setv removed_apple short_neighbor)
    ))
    ; add apples remove

    (if (= enemyPoint short_neighbor)(
      scores.append (float "-inf")
    ) (
      do
      (scores.append (+ "branch: " (str(minTurn (- deep 1) short_neighbor enemyPoint temp_score))))
    ))
    (if (not(is removed_apple None))(
      apples.append short_neighbor
    ))
  ))
  scores
))

(defn minTurn[deep userPoint enemyPoint score](
  do
  (setv enemy (cut (makeEnemyTurn enemyPoint userPoint) 0 2))
  (if (= enemy userPoint)(
    return (float "-inf")
  )(
    return (maxTurn deep userPoint enemy score)
  ))
))


(for [x (range (len (pacmanAction)))](
  do
	(print (get (pacmanAction) x))
))