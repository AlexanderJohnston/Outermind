import Timeline from "totem-timeline";

export default {
  surface: Timeline.query(class {
    grid = {
      rows: 42,
      columns: 84,
      cellWidth: 25,
      cellHeight: 25
    };
  })
};