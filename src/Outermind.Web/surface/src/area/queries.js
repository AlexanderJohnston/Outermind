import Timeline from "totem-timeline";

export default {
    layoutOffsets: Timeline.query({
      name: "LayoutOffsets",
      props: {
          leftOffset: 0,
          topOffset: 0,
      },
      given: {
        layoutOffsetsChanged(e){
          if (e.left)
            this.leftOffset = e.left;
          if (e.top)
            this.topOffset = e.top;
        }
      }        
    })
  };