export default {
  startNewCard,
  resizeNewCard,
  selectCard,
  resizeCard,
  dragCard
};

function startNewCard(grid, originX, originY) {
  return {
    row: rowFromY(grid, originY),
    column: columnFromX(grid, originX),
    rows: 1,
    columns: 1,
    elevation: 3
  };
}

function resizeNewCard(grid, cards, newCard, originX, originY, x, y) {
  let startX = Math.min(originX, x);
  let startY = Math.min(originY, y);

  let endX = Math.max(originX, x);
  let endY = Math.max(originY, y);

  let startRow = rowFromY(grid, startY);
  let startColumn = columnFromX(grid, startX);

  let endRow = rowFromY(grid, endY);
  let endColumn = columnFromX(grid, endX);

  let area = {
    row: startRow,
    column: startColumn,
    rows: endRow - startRow + 1,
    columns: endColumn - startColumn + 1
  };

  if(area.rows === newCard.rows && area.columns === newCard.columns) {
    return newCard;
  }

  let elevation = elevateArea(area, cards);

  return { ...area, elevation };
}

function selectCard(cards, card) {
  let others = cards
    .filter(({ id }) => id !== card.id)
    .sort((a, b) => a.elevation - b.elevation);

  let elevations = { [card.id]: 3 };

  for(let i = 0; i < others.length; i++) {
    let other = others[i];

    elevations[other.id] = 3;

    for(let j = 0; j < i; j++) {
      let prior = others[j];

      if(areasOverlap(prior, other)) {
        elevations[other.id] = elevations[prior.id] + 3;
      }
    }

    if(areasOverlap(card, other)) {
      elevations[card.id] = elevations[other.id] + 3;
    }
  }

  return [card, ...others].map(original => {
    let elevation = elevations[original.id];

    return elevation === original.elevation ?
      original :
      { ...original, elevation };
  });
}

function resizeCard(grid, cards, card, hover, x, y) {
  let row;
  let rows;

  if(hover.north) {
    row = rowFromY(grid, y);
    rows = card.rows + (card.row - row);
  }
  else if(hover.south) {
    row = card.row;
    rows = rowFromY(grid, y) - card.row + 1;
  }
  else {
    row = card.row;
    rows = card.rows;
  }

  let column;
  let columns;

  if(hover.west) {
    column = columnFromX(grid, x);
    columns = card.columns + (card.column - column);
  }
  else if(hover.east) {
    column = card.column;
    columns = columnFromX(grid, x) - card.column + 1;
  }
  else {
    column = card.column;
    columns = card.columns;
  }

  if(row === card.row
    && column === card.column
    && rows === card.rows
    && columns === card.columns) {
    return cards;
  }

  let resized = { ...card, row, column, rows, columns };

  return selectCard(cards, resized);
}

function dragCard(grid, cards, card, x, y) {
  let row = rowFromY(grid, y);
  let column = columnFromX(grid, x);

  if(row + card.rows > grid.rows) {
    row = grid.rows - card.rows + 1;
  }

  if(column + card.columns > grid.columns) {
    column = grid.columns - card.columns + 1;
  }

  if(row === card.row && column === card.column) {
    return cards;
  }

  return selectCard(cards, { ...card, row, column });
}

//
// Details
//

function rowFromY(grid, y) {
  let row = Math.ceil(y / grid.cellHeight);

  return Math.max(1, Math.min(row, grid.rows));
}

function columnFromX(grid, x) {
  let column = Math.ceil(x / grid.cellWidth);

  return Math.max(1, Math.min(column, grid.columns));
}

function areasOverlap(area0, area1) {
  let area0EndRow = area0.row + area0.rows - 1;
  let area0EndColumn = area0.column + area0.columns - 1;

  let area1EndRow = area1.row + area1.rows - 1;
  let area1EndColumn = area1.column + area1.columns - 1;

  return !(
    area0.row > area1EndRow
    || area0.column > area1EndColumn
    || area1.row > area0EndRow
    || area1.column > area0EndColumn);
}

function elevateArea(area, others) {
  let overlapElevations = others
    .filter(other => areasOverlap(area, other))
    .map(other => other.elevation)
    .concat(0);

  return Math.max(...overlapElevations) + 3;
}