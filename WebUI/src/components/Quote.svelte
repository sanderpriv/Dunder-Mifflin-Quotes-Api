<script>
    import {fade} from 'svelte/transition';

    export let quote;
    let speaker = quote?.speaker;
    let season = quote?.season;
    let episode = quote?.episode;

    let showQuote = false;
    setTimeout(() => showQuote = true, 1000)
</script>

{#if (!showQuote)}
    <div class="loading">
        Loading...
    </div>
{:else}
    <div class="quote" transition:fade>
        <b>"{quote.lineText}"</b>
        <img src="src/lib/images/cast/{speaker}.jpg" alt="Portrait of {speaker} from The Office"/>
        <span>- {speaker}</span>
        <span>Season {season} Episode {episode}</span>
    </div>
{/if}

<style>
    .loading {
        margin-block: 4rem;
    }

    .quote {
        margin-block: 4rem;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    img {
        margin-block: 2rem;
        height: 100%;
        width: 100%;
    }

    img {
        margin-top: 2rem;
        height: 100%;
        width: 100%;
    }
</style>