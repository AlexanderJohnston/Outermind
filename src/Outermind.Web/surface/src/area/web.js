import Timeline from "totem-timeline";
export default {
    cardStack: Timeline.webQuery("stackRenewed", args => args.stackEndpoint),
    wildCard: Timeline.webQuery("wildCard", args => args.endpoint),
}