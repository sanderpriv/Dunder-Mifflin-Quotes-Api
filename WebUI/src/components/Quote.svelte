<script>
    import {fade} from 'svelte/transition';

    export let quote;
</script>


<div class="quote" in:fade>
    <b>"{quote.lineText}"</b>
    <img src="src/lib/images/cast/{quote.speaker}.jpg" alt="Portrait of {quote.speaker} from The Office"/>
    <span>- {quote.speaker}</span>
    <span>Season {quote.season} Episode {quote.episode}</span>
</div>

<style>

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