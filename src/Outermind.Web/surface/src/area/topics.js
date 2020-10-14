import Timeline from 'totem-timeline';

Timeline.topic({
    name: "CardWatcher",
    data() {
        return {
            cards: [],
        }
    },
    when: {
        async stackRenewed(e) {
            this.cards = e.stack;
        },
        async moveCard(e) {
            console.log('moving card from topic');
            await Timeline.http.postJson('/api/card/move', { body: e.card });
        },
        async updateCard(e) {
            console.log('updating');
            await Timeline.http.postJson('/api/card/update', {body: e.card});
        },
        async updateCards(e) {
            console.log('updating all cards');
            //await Timeline.http.putJson('/api/card/updateAll', {body: e.cards});
        },
    },
})