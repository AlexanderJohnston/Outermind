import Timeline from "totem-timeline";

export default {
    layoutOffsets: Timeline.query({
      name: "LayoutOffsets",
      data: function() {
        return {
            leftOffset: 300,
            topOffset: 270,
        }
      },
      given: {
        layoutOffsetsChanged(e){
          console.log(`left: ${e.left}, top: ${e.top}`);
          if (e.left !== null) {
            console.log(`left: ${e.left}`);
            this.leftOffset = e.left;
          }
          if (e.top !== null)
          {
            console.log(`top: ${e.top}`);
            this.topOffset = e.top;
          }
        }
      }        
    }),
    selectedCard: Timeline.query({
        name: "SelectedCard",
        data: function() {
            return {
                selectedCard: {id: null, row: 0, rows: 0, column: 0, columns: 0, endpoint: ''},
            }
        },
        given: {
            openCard(e) {
                console.log('selected');
                this.selectedCard = e.card;
            }
        }
    }),
    updatedCard: Timeline.query({
        name: "UpdatedCard",
        props: ["updatedCard"],
        given: {
            updateCard(e) {
                this.updatedCard = e.card;
            }
        }
    }),
  };