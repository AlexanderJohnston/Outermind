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
            await Timeline.http.postJson('/api/card/update', {body: e.card});
        },
    },
})