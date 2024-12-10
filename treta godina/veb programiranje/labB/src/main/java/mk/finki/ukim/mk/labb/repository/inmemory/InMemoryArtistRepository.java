package mk.finki.ukim.mk.labb.repository;

import mk.finki.ukim.mk.labb.bootstrap.DataHolder;
import mk.finki.ukim.mk.labb.model.Artist;
import mk.finki.ukim.mk.labb.model.Song;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public class InMemoryArtistRepository
{
    public List<Artist> findAll(){
        return DataHolder.artists;
    }



    public Optional<Artist> findById(Long id){
        return DataHolder.artists.stream()
                .filter(c-> c.getId().equals(id))
                .findFirst();
    }

    public Song addSongToArtist(Artist artist, Song song){


        DataHolder.artists.stream().filter(c->c.getId().equals(artist.getId()))
                .findFirst().ifPresent(c->c.getSongs().add(song));

        return DataHolder.songs.stream().filter(c->c.trackId.equals(song.getTrackId())).findFirst().orElse(null);

    }

}
