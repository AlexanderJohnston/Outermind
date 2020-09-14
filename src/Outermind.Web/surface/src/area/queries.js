import Timeline from "totem-timeline";

export default {
    theStack: Timeline.query({
        name: "TheStack",
        data: function() {
            return {
                cards: [],
            };
        },
        given: {
            stackRenewed(e) {
                this.cards = Object.values(e.stack);
            }
        },
    }),
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
                this.selectedCard = e.card;
            }
        }
    }),
    updatedCard: Timeline.query({
        name: "UpdatedCard",
        props: ["updatedCard"],
        given: {
            updateCard(e) {
                console.log('did it work?');
                this.updatedCard = e.card;
            }
        }
    }),
  };