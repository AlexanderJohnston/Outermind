import Timeline from "totem-timeline";

export default {
  surface: Timeline.query(class {
    rows = 50;
    columns = 100;
    cellWidth = 25;
    cellHeight = 25;
  })
};