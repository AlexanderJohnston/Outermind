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