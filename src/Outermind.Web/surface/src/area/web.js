import Timeline from "totem-timeline";
export default {
    cardStack: Timeline.webQuery("stackRenewed", args => args.stackEndpoint),
}